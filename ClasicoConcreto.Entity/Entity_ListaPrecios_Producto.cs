// Decompiled with JetBrains decompiler
// Type: ClasicoConcreto.Entity.Entity_ListaPrecios_Producto
// Assembly: ClasicoConcreto.Entity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 88FB6C84-08B1-4E89-936D-55DC20DF6C02
// Assembly location: C:\Users\jsoto\Desktop\Recuperacion Concreto\ClasicoConcreto.Entity.dll

using System;

namespace ClasicoConcreto.Entity
{
    public class Entity_ListaPrecios_Producto
    {
        private int _intLista;
        private int _intProducto;
        private Decimal _dblMenudeo;
        private Decimal _dblMedioMayoreo;
        private Decimal _dblMayoreo;
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
