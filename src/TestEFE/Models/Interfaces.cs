//-----------------------------------------------------------------------
// <copyright file="D:\PROJEKTE\Bestand\Importer\Tools\GenericImport\src\TestEFE\Models\Interfaces.cs" company="AXA Partners">
// Author: Jörg H Primke
// Copyright (c) 2021 - AXA Partners. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Linq;

namespace TestEFE.Models
{
    public interface IId
    {
        int Id { get; set; }
    }

    public interface IChangeableData : IId
    {
        DateTimeOffset LastChanged { get; set; }
    }

    public interface ITraceableData : IChangeableData, IId
    {
        string CreatedBy { get; set; }

        string ChangedBy { get; set; }

        DateTimeOffset Created { get; set; }
    }

    public interface IPolicyData : IChangeableData, IId
    {
        string PolicyNumber { get; set; }

        int ContractId { get; set; }

        bool Active { get; set; }
    }
}
