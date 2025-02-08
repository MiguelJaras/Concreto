// Decompiled with JetBrains decompiler
// Type: ClasicoConcreto.Entity.Entity_CotizacionDet
// Assembly: ClasicoConcreto.Entity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 88FB6C84-08B1-4E89-936D-55DC20DF6C02
// Assembly location: C:\Users\jsoto\Desktop\Recuperacion Concreto\ClasicoConcreto.Entity.dll

using System;

namespace ClasicoConcreto.Entity
{
  public class Entity_CotizacionDet
  {
    private int _intPartida;
    private int _intCotizacion;
    private int _intProducto;
    private int _intTipo;
    private Decimal _decCantidad;
    private Decimal _decPrecio;
    private Decimal _decTotal;
    private DateTime _datFechaAlta;
    private int _intUsuarioAlta;
    private string _strMaquinaAlta;

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

    public int intCotizacion
    {
      get
      {
        return this._intCotizacion;
      }
      set
      {
        this._intCotizacion = value;
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

    public int intTipo
    {
      get
      {
        return this._intTipo;
      }
      set
      {
        this._intTipo = value;
      }
    }

    public Decimal decCantidad
    {
      get
      {
        return this._decCantidad;
      }
      set
      {
        this._decCantidad = value;
      }
    }

    public Decimal decPrecio
    {
      get
      {
        return this._decPrecio;
      }
      set
      {
        this._decPrecio = value;
      }
    }

    public Decimal decTotal
    {
      get
      {
        return this._decTotal;
      }
      set
      {
        this._decTotal = value;
      }
    }

    public DateTime datFechaAlta
    {
      get
      {
        return this._datFechaAlta;
      }
      set
      {
        this._datFechaAlta = value;
      }
    }

    public int intUsuarioAlta
    {
      get
      {
        return this._intUsuarioAlta;
      }
      set
      {
        this._intUsuarioAlta = value;
      }
    }

    public string strMaquinaAlta
    {
      get
      {
        return this._strMaquinaAlta;
      }
      set
      {
        this._strMaquinaAlta = value;
      }
    }
  }
}
