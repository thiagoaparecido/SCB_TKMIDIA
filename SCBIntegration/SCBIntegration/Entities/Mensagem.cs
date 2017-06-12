using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCBIntegration.Entities
{
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)] 
    public partial class Mensagem
    {

        private string tipoMensagemField;

        private string codigoMensagemField;

        private string textoMensagemField;

        private string dataHoraInicioField;

        /// <remarks/>
        public string tipoMensagem
        {
            get
            {
                return this.tipoMensagemField;
            }
            set
            {
                this.tipoMensagemField = value;
            }
        }

        /// <remarks/>
        public string codigoMensagem
        {
            get
            {
                return this.codigoMensagemField;
            }
            set
            {
                this.codigoMensagemField = value;
            }
        }

        /// <remarks/>
        public string textoMensagem
        {
            get
            {
                return this.textoMensagemField;
            }
            set
            {
                this.textoMensagemField = value;
            }
        }

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

    }
}
