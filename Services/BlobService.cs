using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;

namespace GameArena.Services;
public interface IBlobService
{
    Task<string?> UploadAsync(IFormFile file, CancellationToken ct = default);
}

public class BlobService : IBlobService
{
    private readonly BlobContainerClient _container;

    public BlobService(IConfiguration config)
    {
        var conn = config["Azure:Blob:ConnectionString"] ?? throw new InvalidOperationException("Azure Blob connection not configured.");
        var containerName = config["Azure:Blob:Container"] ?? "images";
        var client = new BlobServiceClient(conn);
        _container = client.GetBlobContainerClient(containerName);
        _container.CreateIfNotExists(PublicAccessType.Blob);
    }

    public async Task<string?> UploadAsync(IFormFile file, CancellationToken ct = default)
    {
        if (file == null || file.Length == 0) return null;

        var blobName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var blobClient = _container.GetBlobClient(blobName);

        var headers = new BlobHttpHeaders { ContentType = file.ContentType ?? "application/octet-stream" };
        await using var stream = file.OpenReadStream();
        await blobClient.UploadAsync(stream, new BlobUploadOptions { HttpHeaders = headers }, ct);

        return blobClient.Uri.ToString();
    }
}