using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCBIntegration.Entities
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class Sessao
    {

        private string dataHoraInicioField;

        private string modalidadeField;

        private VendedorRemoto vendedorRemotoField;

        private Obra[] obrasField;

        private TotalizacaoTipoAssento[] totalizacoesTipoAssentoField;

        /// <remarks/>
        public string dataHoraInicio
        {
            get
            {
                return this.dataHoraInicioField;
            }
            set
            {
                this.dataHoraInicioField = value;
            }
        }

        /// <remarks/>
        public string modalidade
        {
            get
            {
                return this.modalidadeField;
            }
            set
            {
                this.modalidadeField = value;
            }
        }

        /// <remarks/>
        public VendedorRemoto vendedorRemoto
        {
            get
            {
                return this.vendedorRemotoField;
            }
            set
            {
                this.vendedorRemotoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("obra", IsNullable = false)]
        public Obra[] obras
        {
            get
            {
                return this.obrasField;
            }
            set
            {
                this.obrasField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("totalizacaoTipoAssento", IsNullable = false)]
        public TotalizacaoTipoAssento[] totalizacoesTipoAssento
        {
            get
            {
                return this.totalizacoesTipoAssentoField;
            }
            set
            {
                this.totalizacoesTipoAssentoField = value;
            }
        }
    }
}
