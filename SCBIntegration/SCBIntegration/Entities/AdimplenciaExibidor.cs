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
    [System.Xml.Serialization.XmlRootAttribute(ElementName ="AdimplenciaExibidor", Namespace = "", IsNullable = false)]
    public partial class AdimplenciaExibidor
    {

        private uint registroANCINEExibidorField;

        private System.DateTime diaCinematograficoField;

        private AdimplenciaSala[] adimplenciaSalasField;

        private StatusRelatorioBilheteria statusRelatorioBilheteriaField;

        /// <remarks/>
        public uint registroANCINEExibidor
        {
            get
            {
                return this.registroANCINEExibidorField;
            }
            set
            {
                this.registroANCINEExibidorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime diaCinematografico
        {
            get
            {
                return this.diaCinematograficoField;
            }
            set
            {
                this.diaCinematograficoField = value; 
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("adimplenciaSala", IsNullable = false)]
        public AdimplenciaSala[] adimplenciaSalas
        {
            get
            {
                return this.adimplenciaSalasField;
            }
            set
            {
                this.adimplenciaSalasField = value;
            }
        }

        /// <remarks/>
        public StatusRelatorioBilheteria statusRelatorioBilheteria
        {
            get
            {
                return this.statusRelatorioBilheteriaField;
            }
            set
            {
                this.statusRelatorioBilheteriaField = value;
            }
        }
    }

    


}
