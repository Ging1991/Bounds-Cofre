using System.Collections.Generic;
using Ging1991.Persistencia.Lectores;
using Ging1991.Persistencia.Lectores.Directos;
using UnityEngine;

namespace Bounds.Cofres {

	public class Cofre {

		private readonly LectorListaCadenas lector;
		private readonly Dictionary<string, CartaCofreBD> cartas;

		public Cofre(string direccion, string direccionRecursos) {
			lector = new LectorListaCadenas(direccion, TipoLector.DINAMICO);
			lector.InicializarDesdeRecursos(direccionRecursos);
			cartas = new Dictionary<string, CartaCofreBD>();
			foreach (string codigo in lector.Leer().valor) {
				CartaCofreBD cartaCofre = new CartaCofreBD(codigo);
				cartas[cartaCofre.GetCodigoIndividual()] = cartaCofre;
			}
		}


		public List<CartaCofreBD> GetCartas() {
			return new List<CartaCofreBD>(cartas.Values);
		}


		public void AgregarCarta(CartaCofreBD carta) {
			string codigo = carta.GetCodigoIndividual();
			if (!cartas.ContainsKey(codigo)) {
				cartas[codigo] = carta;
			}
			else {
				cartas[codigo].cantidad += carta.cantidad;
			}
			if (cartas[codigo].cantidad > 5)
				cartas[codigo].cantidad = 5;
		}


		public void RemoverCarta(CartaCofreBD carta) {
			if (!cartas.ContainsKey(carta.GetCodigoIndividual())) {
				Debug.LogWarning("Se intentó quitar del cofre una carta que no tenía.");
			}
			else {
				cartas[carta.GetCodigoIndividual()].cantidad -= carta.cantidad;
				if (cartas[carta.GetCodigoIndividual()].cantidad <= 0)
					cartas.Remove(carta.GetCodigoIndividual());
			}
		}


		public void Guardar() {
			List<string> codigos = new List<string>();
			foreach (CartaCofreBD linea in cartas.Values) {
				codigos.Add(linea.GetCodigo());
			}
			codigos.Sort();
			lector.Guardar(codigos);
		}


		public int GetCantidadCartasDiferentes(int cartaID) {
			int cantidad = 0;
			foreach (CartaCofreBD linea in GetCartas()) {
				if (linea.cartaID == cartaID) {
					cantidad += linea.cantidad;
				}
			}
			return cantidad;
		}


		public int GetCantidadCartasPorColeccion(string codigo) {
			int cantidad = 0;
			foreach (CartaCofreBD linea in cartas.Values) {
				if (linea.GetCodigoColeccion() == codigo) {
					cantidad += linea.cantidad;
				}
			}
			return cantidad;
		}


		public int GetCantidadCartasPorColeccion(List<string> codigos) {
			int cantidad = 0;
			List<string> codigosActuales = new List<string>();
			foreach (CartaCofreBD carta in cartas.Values) {
				if (!codigosActuales.Contains(carta.GetCodigoColeccion())) {
					codigosActuales.Add(carta.GetCodigoColeccion());
				}
			}
			foreach (string codigo in codigosActuales) {
				if (codigos.Contains(codigo)) {
					cantidad++;
				}
			}
			return cantidad;
		}


		public int GetCantidadCartasDiferentes(List<int> cartas) {
			int cantidad = 0;
			List<int> cartasIDDiferentes = new List<int>();
			foreach (CartaCofreBD linea in GetCartas()) {
				if (!cartasIDDiferentes.Contains(linea.cartaID)) {
					cartasIDDiferentes.Add(linea.cartaID);
				}
			}
			foreach (int cartaID in cartasIDDiferentes) {
				if (cartas.Contains(cartaID)) {
					cantidad++;
				}
			}
			return cantidad;
		}


	}

}