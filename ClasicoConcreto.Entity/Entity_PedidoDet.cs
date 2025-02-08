// Decompiled with JetBrains decompiler
// Type: ClasicoConcreto.Entity.Entity_PedidoDet
// Assembly: ClasicoConcreto.Entity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 88FB6C84-08B1-4E89-936D-55DC20DF6C02
// Assembly location: C:\Users\jsoto\Desktop\Recuperacion Concreto\ClasicoConcreto.Entity.dll

using System;

namespace ClasicoConcreto.Entity
{
  public class Entity_PedidoDet
  {
    private int _intPartida;
    private int _intPedido;
    private string _strHoraEntrega;
    private int _intProducto;
    private string _item_Code;
    private Decimal _dblCantidad;
    private Decimal _dblPrecio;
    private string _UOM;

    public int intPartida
    {
      get
      {
        return this._intPartida;
      }
      set
      {
        this._intPartida = value;
      }
    }

    public int intPedido
    {
      get
      {
        return this._intPedido;
      }
      set
      {
        this._intPedido = value;
      }
    }

    public string strHoraEntrega
    {
      get
      {
        return this._strHoraEntrega;
      }
      set
      {
        this._strHoraEntrega = value;
      }
    }

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

    public string item_Code
    {
      get
      {
        return this._item_Code;
      }
      set
      {
        this._item_Code = value;
      }
    }

    public Decimal dblCantidad
    {
      get
      {
        return this._dblCantidad;
      }
      set
      {
        this._dblCantidad = value;
      }
    }

    public Decimal dblPrecio
    {
      get
      {
        return this._dblPrecio;
      }
      set
      {
        this._dblPrecio = value;
      }
    }

    public string UOM
    {
      get
      {
        return this._UOM;
      }
      set
      {
        this._UOM = value;
      }
    }
  }
}
