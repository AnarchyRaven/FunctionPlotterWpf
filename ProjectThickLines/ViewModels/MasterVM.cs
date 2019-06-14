//-----------------------------------------------------------------------
// <copyright file="MasterVM.cs" company="FH Wiener Neustadt">
//     Copyright (c) Emre Rauhofer. All rights reserved.
// </copyright>
// <author>Emre Rauhofer</author>
// <summary>
// This program is a plot. 
// </summary>
//-----------------------------------------------------------------------
namespace ProjectThickLines.ViewModels
{
    using System;
    using System.IO;
    using System.Windows;
    using System.Windows.Input;
    using ProjectThickLines.Commands;

    /// <summary>
    /// The <see cref="MasterVM"/> class.
    /// </summary>
    [Serializable]
    public class MasterVM
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MasterVM"/> class.
        /// </summary>
        public MasterVM()
        {
            this.LoadOldAppState();
        }

        /// <summary>
        /// Gets or sets the grid for the view.
        /// </summary>
        /// <value> A normal <see cref="GridVM"/>. </value>
        public GridVM Grid
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the function list for the view.
        /// </summary>
        /// <value> A normal <see cref="FunctionListVM"/>. </value>
        public FunctionListVM FunctionListVM
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Zoom window.
        /// </summary>
        /// <value> A normal <see cref="ZoomVM"/>. </value>
        public ZoomVM ZoomVM
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the command to save the app status.
        /// </summary>
        /// <value> A normal <see cref="ICommand"/>. </value>
        public ICommand SaveApp
        {
            get
            {
                return new Command(obj =>
                {
                    try
                    {
                        var filePath = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString();
                        filePath = filePath + @"\app.dat";
                        ApplicationSerealizer.Save(filePath, new MasterVMContainer(this.Grid, this.FunctionListVM));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occured: " + ex);
                    }
                });
            }
        }

        /// <summary>
        /// This method loads the old app state.
        /// </summary>
        private void LoadOldAppState()
        {
            this.ZoomVM = new ZoomVM();
            var filePath = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString();
            filePath = filePath + @"\app.dat";
            MasterVMContainer b = ApplicationSerealizer.Load(filePath);
            if (b != null)
            {
                this.Grid = new GridVM(b.GridVMContainer);
                this.FunctionListVM = new FunctionListVM(b.FunctionalListVMContainer, this.Grid.SmallestXValue, this.Grid.BigestXValue, this.Grid.SmallestYValue, this.Grid.BigestYValue);
            }
            else
            {
                this.Grid = new GridVM();
                this.FunctionListVM = new FunctionListVM();
                this.FunctionListVM.SmallestXValueGrid = this.Grid.SmallestXValue;
                this.FunctionListVM.BigestXValueGrid = this.Grid.BigestXValue;
                this.FunctionListVM.SmallestYValueGrid = this.Grid.SmallestYValue;
                this.FunctionListVM.BigestYValueGrid = this.Grid.BigestYValue;
            }

            this.Grid.OnXYValueChanged += this.FunctionListVM.ChangeXYValueGrid;
            this.FunctionListVM.OnYMinChanged += this.Grid.ChangeYMinValue;
            this.FunctionListVM.OnYMaxChanged += this.Grid.ChangeYMaxValue;
            this.ZoomVM.OnXYChanged += this.Grid.Zoom;
            this.FunctionListVM.DrawNewPolyLines();
        }
    }
}
