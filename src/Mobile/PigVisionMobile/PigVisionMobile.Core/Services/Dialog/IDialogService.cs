using System.Threading.Tasks;

namespace PigVisionMobile.Services
{
    public interface IDialogService
    {
        Task ShowAlertAsync(string message, string title, string buttonLabel);
    }
}
