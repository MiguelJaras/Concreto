// Decompiled with JetBrains decompiler
// Type: ClasicoConcreto.Entity.Entity_Producto
// Assembly: ClasicoConcreto.Entity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 88FB6C84-08B1-4E89-936D-55DC20DF6C02
// Assembly location: C:\Users\jsoto\Desktop\Recuperacion Concreto\ClasicoConcreto.Entity.dll

using System;

namespace ClasicoConcreto.Entity
{
  public class Entity_Producto : EntityBaseClass
  {
    private int _intProducto;
    private string _strNombre;
    private Decimal _dblMenudeo;
    private Decimal _dblMedioMayoreo;
    private Decimal _dblMayoreo;
    private bool _bEstatus;

    public int intProducto
    {
      get
      {
        return this._intProducto;
      }
      set
      {
        this._intProducto = value;
      }
    }

    public string strNombre
    {
      get
      {
        return this._strNombre;
      }
      set
      {
        this._strNombre = value;
      }
    }

    public Decimal dblMenudeo
    {
      get
      {
        return this._dblMenudeo;
      }
      set
      {
        this._dblMenudeo = value;
      }
    }

    public Decimal dblMedioMayoreo
    {
      get
      {
        return this._dblMedioMayoreo;
      }
      set
      {
        this._dblMedioMayoreo = value;
      }
    }

    public Decimal dblMayoreo
    {
      get
      {
        return this._dblMayoreo;
      }
      set
      {
        this._dblMayoreo = value;
      }
    }

    public bool bEstatus
    {
      get
      {
        return this._bEstatus;
      }
      set
      {
        this._bEstatus = value;
      }
    }
  }
}
