using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSchnorrSignature
{
    [VVM(typeof(ViewModel))]
    internal class ViewModel : BaseViewModel
    {
        private string v, a, p, q, h, s1, s2, m;
        private Command generateParams = new Command(),
                        freadPublicParams = new Command(),
                        fwritePublicParams = new Command(),
                        freadPrivateKey = new Command(),
                        fwritePrivateKey = new Command(),
                        generateSignature = new Command(),
                        freadSignature = new Command(),
                        fwriteSignature = new Command(),
                        verifySignature = new Command(),

                        loadData = new Command(),
                        storeData = new Command();




        private readonly Model model;

        public ViewModel()
        {
            this.model = new Model();
            this.generateParams.ExecuteReceived += GenerateParams_ExecuteReceived;
            this.freadPublicParams.ExecuteReceived += FreadPublicParams_ExecuteReceived;
            this.fwritePublicParams.ExecuteReceived += FwritePublicParams_ExecuteReceived;
            this.freadPrivateKey.ExecuteReceived += FreadPrivateKey_ExecuteReceived;
            this.fwritePrivateKey.ExecuteReceived += FwritePrivateKey_ExecuteReceived;
            this.generateSignature.ExecuteReceived += GenerateSignature_ExecuteReceived;
            this.freadSignature.ExecuteReceived += FreadSignature_ExecuteReceived;
            this.fwriteSignature.ExecuteReceived += FwriteSignature_ExecuteReceived;
            this.verifySignature.ExecuteReceived += VerifySignature_ExecuteReceived;

            this.loadData.ExecuteReceived += LoadData_ExecuteReceived;
            this.storeData.ExecuteReceived += StoreData_ExecuteReceived;

            /*this.v = this.model.V.ToString("x");
            this.a = this.model.A.ToString("x");
            this.p = this.model.P.ToString("x");
            this.q = this.model.Q.ToString("x");
            this.h = this.model.H.ToString("x");*/

            /*this.model.GetSigningParams(out v, out a, out p, out q, out h);
            this.s1 = this.model.S1.ToString("x");
            this.s2 = this.model.S2.ToString("x");
            this.m = Encoding.ASCII.GetString(this.model.M);*/
            this.model.GetAllParams(out v, out a, out p, out q, out h, out s1, out s2, out m);
        }

        private void StoreData_ExecuteReceived(object? sender, EventArgs e)
        {
            string? path = ViewModel.WindowService.ShowSaveFileDialog();
            if (path != null)
            {
                this.model.StoreM(path);
                ViewModel.WindowService.ShowMessage($"Saved message data to {path}");
            }
        }

        private void LoadData_ExecuteReceived(object? sender, EventArgs e)
        {
            string? path = ViewModel.WindowService.ShowOpenFileDialog();
            if (path != null)
            {
                this.model.LoadM(path);
                //this.m = Encoding.ASCII.GetString(this.model.M);
                this.model.GetMessage(out this.m);
                OnPropertyChanged(nameof(M));
            }
        }

        private void VerifySignature_ExecuteReceived(object? sender, EventArgs e)
        {
            this.model.SetVerificationParams(v, p, q, h, s1, s2);
            this.model.SetMessage(m);
            bool verified = this.model.VerifySignature();
            if (verified) {
                ViewModel.WindowService.ShowMessage("Podpis zweryfikowany");
            }
            else
            {
                ViewModel.WindowService.ShowMessage("Podpis niezweryfikowany");
            }
        }

        private void FwriteSignature_ExecuteReceived(object? sender, EventArgs e)
        {
            string? path = ViewModel.WindowService.ShowSaveFileDialog();
            if (path != null)
            {
                this.model.StoreSignature(path);
                ViewModel.WindowService.ShowMessage($"Saved signature to {path}");
            }
        }

        private void FreadSignature_ExecuteReceived(object? sender, EventArgs e)
        {
            string? path = ViewModel.WindowService.ShowOpenFileDialog();
            if (path != null)
            {
                this.model.LoadSignature(path);
                this.model.GetVerificationParams(out v, out p, out q, out h, out s1, out s2);
                OnPropertyChanged(nameof(V));
                OnPropertyChanged(nameof(P));
                OnPropertyChanged(nameof(Q));
                OnPropertyChanged(nameof(H));
                OnPropertyChanged(nameof(S1));
                OnPropertyChanged(nameof(S2));
            }
        }

        private void GenerateSignature_ExecuteReceived(object? sender, EventArgs e)
        {
            this.model.SetSigningParams(v, a, p, q, h);
            this.model.SetMessage(m);
            this.model.GenerateSignature();
            //this.model.GetSigningParams(out v, out a, out p, out q, out h);
            this.model.GetVerificationParams(out v, out p, out q, out h, out s1, out s2);
            OnPropertyChanged(nameof(V));
            OnPropertyChanged(nameof(P));
            OnPropertyChanged(nameof(Q));
            OnPropertyChanged(nameof(H));
            OnPropertyChanged(nameof(S1));
            OnPropertyChanged(nameof(S2));
            ViewModel.WindowService.ShowMessage("Signature ready");
        }

        private void FwritePrivateKey_ExecuteReceived(object? sender, EventArgs e)
        {
            string? path = ViewModel.WindowService.ShowSaveFileDialog();
            if (path != null) { 
                this.model.StoreA(path);
                ViewModel.WindowService.ShowMessage($"Saved private key to {path}");
            }
        }

        private void FreadPrivateKey_ExecuteReceived(object? sender, EventArgs e)
        {
            string? path = ViewModel.WindowService.ShowOpenFileDialog();
            if (path != null)
            {
                this.model.LoadA(path);
                this.model.GetPrivateKey(out a);
                OnPropertyChanged(nameof(A));
            }
        }

        private void FwritePublicParams_ExecuteReceived(object? sender, EventArgs e)
        {
            string? path = ViewModel.WindowService.ShowSaveFileDialog();
            if (path != null)
            {
                this.model.StoreVPQH(path);
                ViewModel.WindowService.ShowMessage($"Saved public params to {path}");
            }
        }

        private void FreadPublicParams_ExecuteReceived(object? sender, EventArgs e)
        {
            string? path = ViewModel.WindowService.ShowOpenFileDialog();
            if (path != null)
            {
                this.model.LoadVPQH(path);
                this.model.GetSigningParams(out v, out a, out p, out q, out h);
                OnPropertyChanged(nameof(V));
                OnPropertyChanged(nameof(P));
                OnPropertyChanged(nameof(Q));
                OnPropertyChanged(nameof(H));
            }
        }

        private void GenerateParams_ExecuteReceived(object? sender, EventArgs e)
        {
            this.model.GenerateParams();
            this.model.GetSigningParams(out v, out a, out p, out q, out h);
            OnPropertyChanged(nameof(V));
            OnPropertyChanged(nameof(A));
            OnPropertyChanged(nameof(P));
            OnPropertyChanged(nameof(Q));
            OnPropertyChanged(nameof(H));
            ViewModel.WindowService.ShowMessage("Params ready");
        }

        public string V
        {
            get { return this.v; }
            set
            {
                this.v = value;
            }
        }
        public string A
        {
            get => this.a;
            set => this.a = value;
        }
        public string P
        {
            get => this.p;
            set => this.p = value;
        }
        public string Q
        {
            get => this.q;
            set => this.q = value;
        }
        public string H
        {
            get => this.h;
            set => this.h = value;
        }
        public string S1
        {
            get => this.s1;
            set=> this.s1 = value;
        }
        public string S2
        {
            get => this.s2;
            set => this.s2 = value;
        }
        public string M
        {
            get => this.m;
            set => this.m = value;
        }

        public Command GenerateParams => this.generateParams;
        public Command FreadPublicParams => this.freadPublicParams;
        public Command FwritePublicParams => this.fwritePublicParams;
        public Command FreadPrivateKey => this.freadPrivateKey;
        public Command FwritePrivateKey => this.fwritePrivateKey;
        public Command GenerateSignature => this.generateSignature;
        public Command FreadSignature=>this.freadSignature;
        public Command FwriteSignature => this.fwriteSignature;
        public Command VerifySignature => this.verifySignature;


        public Command LoadData => this.loadData;
        public Command StoreData => this.storeData;
    }
}
