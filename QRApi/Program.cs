using QRCoder;
using System.Drawing;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

app.MapGet("/QRGenerator/{text}", (string text) =>
{
    QRCodeGenerator qrGenerator = new QRCodeGenerator();
    QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
    QRCode qrCode = new QRCode(qrCodeData);
    Image qrCodeImage = qrCode.GetGraphic(20);
    var base64 = ImageToBase64(qrCodeImage);
    return base64;
});

app.Run();

string ImageToBase64(System.Drawing.Image imageIn)
{
    MemoryStream ms = new MemoryStream();
    imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
    return Convert.ToBase64String(ms.GetBuffer());
}