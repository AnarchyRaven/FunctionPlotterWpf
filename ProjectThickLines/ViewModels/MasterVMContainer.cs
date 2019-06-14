//-----------------------------------------------------------------------
// <copyright file="MasterVMContainer.cs" company="FH Wiener Neustadt">
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

    /// <summary>
    /// The <see cref="MasterVMContainer"/> class.
    /// </summary>
    [Serializable]
    public class MasterVMContainer
    {
        /// <summary>
        /// The grid container.
        /// </summary>
        public readonly GridVMContainer GridVMContainer;

        /// <summary>
        /// The <see cref="FunctionListVM"/> container.
        /// </summary>
        public readonly FunctionalListVMContainer FunctionalListVMContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="MasterVMContainer"/> class.
        /// </summary>
        /// <param name="gridVM"> The grid of the view. </param>
        /// <param name="functionListVM"> The functions of the view. </param>
        public MasterVMContainer(GridVM gridVM, FunctionListVM functionListVM)
        {
            this.GridVMContainer = new GridVMContainer(gridVM);
            this.FunctionalListVMContainer = new FunctionalListVMContainer(functionListVM);
        }
    }
}
