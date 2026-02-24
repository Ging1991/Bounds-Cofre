namespace Bounds.Cofres {

	public class CartaCofreBD {

		public int cartaID;
		public int cantidad;
		public string rareza;
		public string imagen;

		public CartaCofreBD(string codigo) {
			var partes = codigo.Split('_');
			cartaID = int.Parse(partes[0]);
			imagen = partes[1];
			rareza = partes[2];
			cantidad = int.Parse(partes[3]);
		}


		public string GetCodigo() {
			return $"{GetCartaIDFormateada()}_{imagen}_{rareza}_{cantidad}";
		}


		public string GetCodigoIndividual() {
			return $"{GetCartaIDFormateada()}_{imagen}_{rareza}";
		}


		public string GetCodigoColeccion() {
			return $"{GetCartaIDFormateada()}_{imagen}";
		}


		protected string GetCartaIDFormateada() {
			string cadenaID = $"{cartaID}";
			if (cartaID < 100)
				cadenaID = $"0{cartaID}";
			if (cartaID < 10)
				cadenaID = $"00{cartaID}";
			return cadenaID;
		}


	}

}