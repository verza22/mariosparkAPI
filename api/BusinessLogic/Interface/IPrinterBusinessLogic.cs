using api.Models;

namespace api.BusinessLogic.Interface
{
    public interface IPrinterBusinessLogic
    {
        List<Printer> GetPrinters(int storeID);
        bool RemovePrinter(int printerID);
        int AddOrUpdatePrinter(Printer printer);
    }
}
