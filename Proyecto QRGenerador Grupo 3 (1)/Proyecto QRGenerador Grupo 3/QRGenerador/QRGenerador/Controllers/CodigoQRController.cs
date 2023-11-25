using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using QRGenerador.Models;

namespace QRGenerador.Controllers
{
    public class CodigoQRController : Controller
    {
        private readonly QrContext _context;
        public CodigoQRController(QrContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Qr(string texto)
        {
            if (string.IsNullOrEmpty(texto))
            {
                // El parámetro texto está vacío, muestra un mensaje de error al usuario
                ViewBag.ErrorMessage = "Por favor ingresa un texto válido";
                return View("Index");
            }
            else
            {
                //El parámetro texto no está vacío, genera el código QR
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(texto, QRCodeGenerator.ECCLevel.Q);
                PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
                byte[] qrCodeImage = qrCode.GetGraphic(20);
                string model = Convert.ToBase64String(qrCodeImage);

                //Valor que devuelve un booleano para determinar si el texto ingresado ya está registrado en la base de datos
                var existe = _context.CodigosQr.Any(cod => cod.URL == texto);

                //Si el texto coincide con uno ya registrado, solo genera el código QR, pero no lo guarda en la base de datos
                if (existe) 
                {
                    return View("Qr", model);
                }
                // Guardar en la base de datos
                QRAtributos qrItem = new QRAtributos
                {
                    URL = texto // Asigna aquí el texto que deseas guardar como URL
                };
                _context.CodigosQr.Add(qrItem);
                _context.SaveChanges();

                return View("Qr", model);
            }
        }

        public IActionResult HistorialQr() 
        {
            var historialQr = _context.CodigosQr.ToList();

            return View(historialQr);
        }
    }
}
