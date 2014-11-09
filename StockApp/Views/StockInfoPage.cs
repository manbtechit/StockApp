﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using StockApp.Data;
using StockApp.ViewModel;
using StockApp.Views;

namespace StockApp.Views
{
    /// <summary>
    /// This page is the main page, and displays financial information about a certain stock.
    /// </summary>
    class StockInfoPage : ContentPage
    {
        /// <summary>
        /// Constructor sets the title page and creates the toolbar with a search and calculate icon
        /// </summary>
        public StockInfoPage(StockService sService, StockServiceViewModel ssvm)
        {
            Title = "Stock Quotr"; 
            createToolbarSearch(ssvm);
            createCalcSearch(ssvm);
            ToolbarItems.Add(calculate);
            ToolbarItems.Add(search);
            
        }
        
        ToolbarItem search;
        ToolbarItem calculate;
        //public StockService servResponse;

        /// <summary>
        /// Creates 2 tool bar items in the navigation
        /// </summary>
        private void createToolbarSearch(StockServiceViewModel ssvm)
        {
            if (search != null)
            {
                return;
            }
            
            //string iconName = Device.OnPlatform(null, "ic_action_search.png", null);
            search = new ToolbarItem("Search", null, async () =>
            {
                await Navigation.PushAsync(new StockSearchPage(ssvm));
            });
            
        }

        private void createCalcSearch(StockServiceViewModel ssvm)
        {
            if (calculate != null)
            {
                return;
            }
            calculate = new ToolbarItem("Calculate", null, async () =>
            {
                await Navigation.PushAsync(new StockCalcPage(ssvm));
            });
        }






        /// <summary>
        /// If a stock symbol has been assigned by the user, their ticker info will be displayed.  If not, instructions will be displayed on how to use the app.
        /// </summary>
        /// <param name="sService"></param>
        private void createLayout(StockService sService)
        {
            if (sService.qSymbol != null)
            {
                
                var quoteObj = sService.getQuote();
                var labelQuote = new Label
                {
                    Text = quoteObj.Name+" "+quoteObj.LastPrice+ " "+quoteObj.PurchasePrice,

                };
                Content = new StackLayout
                {
                    //Spacing = 10,
                    VerticalOptions = LayoutOptions.Center,
                    Children = { labelQuote }
                };
                
            }
            else
            {
                var labelTest = new Label
                {
                    Text = "Click on the Search button to get the ticker price of a stock.  Then, click on the calculator button to determine your profit or loss."//sService.qSymbol

                };
                Content = new StackLayout
                {
                    //Spacing = 10,
                    VerticalOptions = LayoutOptions.Center,
                    Children = { labelTest }
                };
            }
        }  

        /// <summary>
        /// A new layout is generated every time the page is loaded.
        /// </summary>
        protected override void OnAppearing()
        {
            
            createLayout(App.sService);

           // var servResponse = new StockService("amd");
            
                       
            
        }
    }

    

}
