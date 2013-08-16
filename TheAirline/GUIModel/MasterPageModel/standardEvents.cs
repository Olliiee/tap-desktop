﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TheAirline.GraphicsModel.PageModel.GeneralModel;
using TheAirline.GUIModel.PagesModel.AirlinePageModel;
using TheAirline.GUIModel.PagesModel.AirlinersPageModel;
using TheAirline.GUIModel.PagesModel.AirportsPageModel;
using TheAirline.GUIModel.PagesModel.AlliancesPageModel;
using TheAirline.GUIModel.PagesModel.PilotsPageModel;
using TheAirline.GUIModel.PagesModel.RoutesPageModel;
using TheAirline.Model.GeneralModel;

namespace TheAirline.GUIModel.MasterPageModel
{
    public partial class standardEvents
    {
        private void RoutesManager_Click(object sender, RoutedEventArgs e)
        {
            PageNavigator.NavigateTo(new PageRoutes());
        }
        private void Airliners_Click(object sender, RoutedEventArgs e)
        {
            PageNavigator.NavigateTo(new PageAirliners());
        }
        private void Airports_Click(object sender, RoutedEventArgs e)
        {
            PageNavigator.NavigateTo(new PageAirports());
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            PageNavigator.NavigateTo(new PageAirline(GameObject.GetInstance().HumanAirline));
        }
        private void Alliances_Click(object sender, RoutedEventArgs e)
        {
            PageNavigator.NavigateTo(new PageAlliances());
        }
        private void Pilots_Click(object sender, RoutedEventArgs e)
        {
            PageNavigator.NavigateTo(new PagePilotsFS());
        }
    }
}
