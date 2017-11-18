using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SharePrice.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SharePrice.ViewModels
{
    public class GraphPageViewModel : BaseViewModel
    {
        private string produtoNome;
        private string ProdutoNome
        {
            get { return this.produtoNome; }
            set
            {
                if (Equals(value, this.produtoNome))
                {
                    return;
                }
                this.produtoNome = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Oferta> ofertas;
        public ObservableCollection<Oferta> OfertasLine
        {
            get { return this.ofertas; }
            set
            {
                if (Equals(value, this.ofertas))
                {
                    return;
                }
                this.ofertas = value;
                OnPropertyChanged();
            }
        }

        public PlotModel areaModel;
        public PlotModel AreaModel
        {
            get { return this.areaModel; }
            set
            {
                if (Equals(value, this.areaModel))
                {
                    return;
                }
                this.areaModel = value;
                OnPropertyChanged();
            }
        }

        public GraphPageViewModel()
        {
            AreaModel = new PlotModel();
        }
        

        public PlotModel CreateAreaChart()
        {
            DateTime menorData = OfertasLine.Min(c => c.DataInicio);
            DateTime maiorData = OfertasLine.Max(c => c.DataInicio);

            var plotModel1 = new PlotModel();
            var areaSeries1 = new AreaSeries();

            var minValue = DateTimeAxis.ToDouble(menorData);
            var maxValue = DateTimeAxis.ToDouble(maiorData);

            plotModel1.Title = ProdutoNome;
            plotModel1.Axes.Add(new DateTimeAxis { Position = AxisPosition.Bottom, Minimum = minValue, Maximum = maxValue, StringFormat = "d/M/y" });

            foreach (var item in OfertasLine){
                areaSeries1.Points.Add(new DataPoint(DateTimeAxis.ToDouble(item.DataInicio), item.Preco));
            }
            plotModel1.Series.Add(areaSeries1);
            return plotModel1;
        }
        
        private AxisPosition CategoryAxisPosition()
        {
            if (typeof(BarSeries) == typeof(ColumnSeries))
            {
                return AxisPosition.Bottom;
            }

            return AxisPosition.Left;
        }

        private AxisPosition ValueAxisPosition()
        {
            if (typeof(BarSeries) == typeof(ColumnSeries))
            {
                return AxisPosition.Left;
            }

            return AxisPosition.Bottom;
        }


        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            OfertasLine = parameters.GetValue<ObservableCollection<Oferta>>("ofertas");
            ProdutoNome = parameters.GetValue<string>("produto");
            
            AreaModel = CreateAreaChart();
        }

    }
}
