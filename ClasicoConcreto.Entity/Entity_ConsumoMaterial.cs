

using System;

namespace ClasicoConcreto.Entity
{
  public class Entity_ConsumoMaterial : EntityBaseClass
  {
        private int _intMaterial;
        private DateTime _datFecha;
        private int _intPlanta;
		private decimal _decConcPremM3;
        private decimal _decAguaLto;
        private decimal _decArena4Ton;
        private decimal _decArena5Ton;
        private decimal _decCal;
        private decimal _decCalFlow100;
        private decimal _decCampOx4060;
        private decimal _decCamcret;
        private decimal _decCamptard;
        private decimal _decCementoTon;
        private decimal _decFibraBolsa;
        private decimal _decColorByFerrox;
        private decimal _decGrava1Ton;
        private decimal _decGrava2Ton;
        private decimal _decImper;
        private decimal _decMortardELto;
        private string _strFecha;
        public int IntMaterial
        {
            get
            {
                return this._intMaterial;
            }
            set
            {
                this._intMaterial = value;
            }
        }


        public DateTime DatFecha
        {
            get
            {
                return this._datFecha;
            }
            set
            {
                this._datFecha = value;
            }
        }


        public string StrDatFecha
        {
            get
            {
                return this._strFecha;
            }
            set
            {
                this._strFecha = value;
            }
        }









        public int IntPlanta
        {
          get
          {
            return this._intPlanta;
          }
          set
          {
            this._intPlanta = value;
          }
        }

        public decimal DecConcPremM3
        {
            get
            {
                return this._decConcPremM3;
            }
            set
            {
                this._decConcPremM3 = value;
            }
        }


        public decimal DecAguaLto
        {
            get
            {
                return this._decAguaLto;
            }
            set
            {
                this._decAguaLto = value;
            }
        }

        public decimal DecArena4Ton
        {
            get
            {
                return this._decArena4Ton;
            }
            set
            {
                this._decArena4Ton = value;
            }
        }

        public decimal DecArena5Ton
        {
            get
            {
                return this._decArena5Ton;
            }
            set
            {
                this._decArena5Ton = value;
            }
        }

        public decimal DecCal
        {
            get
            {
                return this._decCal;
            }
            set
            {
                this._decCal = value;
            }
        }

        public decimal DecCalFlow100
        {
            get
            {
                return this._decCalFlow100;
            }
            set
            {
                this._decCalFlow100 = value;
            }
        }


        public decimal DecCampOx4060
        {
            get
            {
                return this._decCampOx4060;
            }
            set
            {
                this._decCampOx4060 = value;
            }
        }

        public decimal DecCamcret
        {
            get
            {
                return this._decCamcret;
            }
            set
            {
                this._decCamcret = value;
            }
        }

        public decimal DecCamptard
        {
            get
            {
                return this._decCamptard;
            }
            set
            {
                this._decCamptard = value;
            }
        }

        public decimal DecCementoTon
        {
            get
            {
                return this._decCementoTon;
            }
            set
            {
                this._decCementoTon = value;
            }
        }



        public decimal DecFibraBolsa
        {
            get
            {
                return this._decFibraBolsa;
            }
            set
            {
                this._decFibraBolsa = value;
            }
        }


        public decimal DecColorByFerrox
        {
            get
            {
                return this._decColorByFerrox;
            }
            set
            {
                this._decColorByFerrox = value;
            }
        }


        public decimal DecGrava1Ton
        {
            get
            {
                return this._decGrava1Ton;
            }
            set
            {
                this._decGrava1Ton = value;
            }
        }

        public decimal decGrava2Ton
        {
            get
            {
                return this._decGrava2Ton;
            }
            set
            {
                this._decGrava2Ton = value;
            }
        }


        public decimal DecImper
        {
            get
            {
                return this._decImper;
            }
            set
            {
                this._decImper = value;
            }
        }


        public decimal DecMortardELto
        {
            get
            {
                return this._decMortardELto;
            }
            set
            {
                this._decMortardELto = value;
            }
        }

    }
}
