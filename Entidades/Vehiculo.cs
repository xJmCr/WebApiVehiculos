﻿namespace WebApiVehiculos.Entidades
{
    public class Vehiculo
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public List<Tipo> Tipos { get; set; }
    }
}
