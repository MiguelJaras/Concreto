// Decompiled with JetBrains decompiler
// Type: ClasicoConcreto.Entity.Entity_Producto
// Assembly: ClasicoConcreto.Entity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 88FB6C84-08B1-4E89-936D-55DC20DF6C02
// Assembly location: C:\Users\jsoto\Desktop\Recuperacion Concreto\ClasicoConcreto.Entity.dll

using System;

namespace ClasicoConcreto.Entity
{
  public class Entity_Precios : EntityBaseClass
  {
    private int _intEmpresa;
    private string _strInsumo;
    private Decimal _dblPrecio;
    private int _intProducto;
    private string _strNombreProducto;
    private string  _strUsuario;
    private string  _strMaquina;
    private DateTime  _datFecha;

        public int IntEmpresa { get => _intEmpresa; set => _intEmpresa = value; }
        public string StrInsumo { get => _strInsumo; set => _strInsumo = value; }
        public decimal DblPrecio { get => _dblPrecio; set => _dblPrecio = value; }
        public int IntProducto { get => _intProducto; set => _intProducto = value; }
        public string StrNombreProducto { get => _strNombreProducto; set => _strNombreProducto = value; }
        public string StrUsuario { get => _strUsuario; set => _strUsuario = value; }
        public string StrMaquina { get => _strMaquina; set => _strMaquina = value; }
        public DateTime DatFecha { get => _datFecha; set => _datFecha = value; }
    }
}
