// Decompiled with JetBrains decompiler
// Type: ClasicoConcreto.Entity.Entity_ListaPrecios_Servicio
// Assembly: ClasicoConcreto.Entity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 88FB6C84-08B1-4E89-936D-55DC20DF6C02
// Assembly location: C:\Users\jsoto\Desktop\Recuperacion Concreto\ClasicoConcreto.Entity.dll

using System;

namespace ClasicoConcreto.Entity
{
  public class Entity_ListaPrecios_Servicio
  {
    private int _intLista;
    private int _intServicio;
    private Decimal _dblPrecio;
    private DateTime _datFechaAlta;
    private int _intClienteAlta;
    private string _strMaquina;

    public int intLista
    {
      get
      {
        return this._intLista;
      }
      set
      {
        this._intLista = value;
      }
    }

    public int intServicio
    {
      get
      {
        return this._intServicio;
      }
      set
      {
        this._intServicio = value;
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

    public int intClienteAlta
    {
      get
      {
        return this._intClienteAlta;
      }
      set
      {
        this._intClienteAlta = value;
      }
    }

    public string strMaquina
    {
      get
      {
        return this._strMaquina;
      }
      set
      {
        this._strMaquina = value;
      }
    }
  }
}
