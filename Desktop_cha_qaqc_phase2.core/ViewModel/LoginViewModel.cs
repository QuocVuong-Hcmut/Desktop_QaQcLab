using Desktop_cha_qaqc_phase2.Core.Commands;
using Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute;
using Desktop_cha_qaqc_phase2.Core.Domain.Stores;
using Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Desktop_cha_qaqc_phase2.core.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public ICommand AddCommand { get; set; }
        private ProductStore _productStore { get; set; }
        public LoginViewModel(ProductStore productStore)
        {
            _productStore=productStore; 
            AddCommand = new RelayCommand(Add);
            //LoginButton.CanExecute = CanLogin;
        }

        private void Add()
        {
            _productStore.Insert(new Product( ) { Name = ProductName , Id = ProductId});
        }


    }
}
