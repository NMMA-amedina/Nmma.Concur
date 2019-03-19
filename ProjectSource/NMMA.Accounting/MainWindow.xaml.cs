using NMMA.Concur;
using System.Windows;

namespace NMMA.Accounting
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnInvoice_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            ManualExec("VendorInvoice");
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
        }

        private void btnPayment_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            ManualExec("PaymentBatch");
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
        }

        private void ManualExec(string mode)
        {
            ConcurProcess proc = new ConcurProcess(mode);
            proc.Process();
            MessageBox.Show("Extract File has been downloaded. Click OK to run SQL job.");
           // proc.CallSQLJob();
        }
       
        public void ConsoleExec(string mode)
        {
            ConcurProcess proc = new ConcurProcess(mode);
            proc.Process();
            this.Close();
            Application.Current.Shutdown();
        }

        private void btnReports_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
