using Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute;
using Desktop_cha_qaqc_phase2.Core.Services.Interfaces;
using ProductVertificationDesktopApp.Core.Domain.Communication;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Domain.Stores
{

    public class ProductStore
    {
        private ObservableCollection<Product> _listProducts = new ObservableCollection<Product>();
        private ObservableCollection<string> _listProductNames = new ObservableCollection<string>();
        private readonly IApiService _apiService;
        private readonly IDialogService _dialogService;

        public ObservableCollection<Product> ListProducts
        {
            get => _listProducts;
            set
            {
                _listProducts = value;
            }
        }

        public ObservableCollection<string> ListProductName
        {
            get => _listProductNames;
            set
            {
                _listProductNames = value;
            }
        }

        public ProductStore(IApiService apiService, IDialogService dialogService)
        {
            _apiService = apiService;
            _dialogService = dialogService;
            Load();
        }
        private async Task Load()
        {
            //_listProducts.Add(new Product() { Id = "HA01", Name = "Nắp đế bàn cầu HA01" });
            //_listProducts.Add(new Product() { Id = "HA02", Name = "Nắp đế bàn cầu HA02" });
            //_listProducts.Add(new Product() { Id = "HA03", Name = "Nắp đế bàn cầu HA03" });
            //_listProducts.Add(new Product() { Id = "HA05", Name = "Nắp đế bàn cầu HA05" });
            _listProductNames.Clear();
            foreach (var product in _listProducts)
            {
                _listProductNames.Add(product.Name);
            };
            try
            {
                var result = await _apiService.GetProduct( );
                if ( result.Resource!=null )
                {

                    _listProducts=result.Resource;
                    foreach ( var product in _listProducts )
                    {
                        _listProductNames.Add(product.Name);
                    }
                }
                else
                {
                    _listProductNames=default;
                    _listProducts=default;
                }

            }
            catch ( Exception ex )
            {
                _dialogService.ShowDialog("Product.Get",2);
            }
        }
        public async Task Insert ( Product product)
        {
            try
            {
                 await _apiService.PostProduct(product);
                Load( );
            }
            catch ( Exception ex )
            {
                _dialogService.ShowDialog("Product.Get",2);
            }
        }
    }
}
