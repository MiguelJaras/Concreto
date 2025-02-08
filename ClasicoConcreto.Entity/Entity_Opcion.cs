// Decompiled with JetBrains decompiler
// Type: ClasicoConcreto.Entity.Entity_Opcion
// Assembly: ClasicoConcreto.Entity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 88FB6C84-08B1-4E89-936D-55DC20DF6C02
// Assembly location: C:\Users\jsoto\Desktop\Recuperacion Concreto\ClasicoConcreto.Entity.dll

namespace ClasicoConcreto.Entity
{
  public class Entity_Opcion
  {
    private short _intOpcion;
    private string _strNombre;
    private string _strURL;
    private bool _bitActivo;

    public short IntOpcion
    {
      get
      {
        return this._intOpcion;
      }
      set
      {
        this._intOpcion = value;
      }
    }

    public string StrNombre
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

    public string StrURL
    {
      get
      {
        return this._strURL;
      }
      set
      {
        this._strURL = value;
      }
    }

    public bool BitActivo
    {
      get
      {
        return this._bitActivo;
      }
      set
      {
        this._bitActivo = value;
      }
    }
  }
}
