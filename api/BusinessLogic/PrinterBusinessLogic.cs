using api.BusinessLogic.Interface;
using api.DataAccess;
using api.Models;

namespace api.BusinessLogic
{
    public class PrinterBusinessLogic : IPrinterBusinessLogic
    {
        private readonly PrinterDataAccess _printerDataAccess;

        public PrinterBusinessLogic(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            _printerDataAccess = new PrinterDataAccess(connectionString);
        }

        public List<Printer> GetPrinters(int storeID)
        {
            return _printerDataAccess.GetPrinters(storeID);
        }

        public bool RemovePrinter(int printerID)
        {
            return _printerDataAccess.RemovePrinter(printerID);
        }

        public int AddOrUpdatePrinter(Printer printer)
        {
            return _printerDataAccess.AddOrUpdatePrinter(printer);
        }

    }
}
