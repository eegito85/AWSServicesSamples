using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AWS.Services.S3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class S3Controller : ControllerBase
    {
        private readonly IAmazonS3 _amazonS3;
        private string _bucketName = "egitobucket";

        public S3Controller(IAmazonS3 amazonS3)
        {
            _amazonS3 = amazonS3;
        }

        [HttpGet("list")]
        public async Task<ActionResult> ListFilesInBucket()
        {
            var files = await _amazonS3.ListObjectsAsync(_bucketName);

            return Ok(files);
        }

        [HttpDelete("deletefile/{filename}")]
        public async Task<ActionResult> DeleteFile(string filename)
        {
            await _amazonS3.DeleteObjectAsync(_bucketName, filename);

            return Ok($"Succesfull delete file {filename}");
        }

        [HttpPost("uploadfile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = file.OpenReadStream(),
                Key = file.FileName,
                BucketName = _bucketName,
                CannedACL = S3CannedACL.NoACL
            };

            using(var fileTransferUtility = new TransferUtility(_amazonS3))
            {
                await fileTransferUtility.UploadAsync(uploadRequest);
            }

            return Ok($"The file {file.FileName} was upload succesfully!");
        }

        [HttpGet("downloadfile/{fileName}")]
        public async Task<ActionResult> DownloadFile(string fileName)
        {
            var getObjectRequest = new GetObjectRequest
            {
                Key = fileName,
                BucketName = _bucketName
            };

            var file = await _amazonS3.GetObjectAsync(getObjectRequest);
            var contentType = file.Headers["Content-Type"];

            byte[] data;
            using(MemoryStream ms = new MemoryStream())
            {
                file.ResponseStream.CopyTo(ms);
                data = ms.ToArray();
            }

            return File(data, contentType, fileName);
        }

        [HttpGet("presignedurl/{fileName}")]
        public async Task<ActionResult> PresignedUrl(string fileName)
        {
            var getUrlRequest = new GetPreSignedUrlRequest {
                Key = fileName,
                BucketName = _bucketName,
                Expires = DateTime.UtcNow.AddDays(1)
            };

            var url = _amazonS3.GetPreSignedURL(getUrlRequest);

            return Ok(url);
        }

    }
}
