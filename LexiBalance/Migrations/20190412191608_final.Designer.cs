﻿// <auto-generated />
using System;
using LexiBalance.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LexiBalance.Migrations
{
    [DbContext(typeof(LexiBalanceContext))]
    [Migration("20190412191608_final")]
    partial class final
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity("LexiBalance.Models.Cliente", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CP");

                    b.Property<string>("Nombre");

                    b.Property<int>("Telefono");

                    b.HasKey("ID");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("LexiBalance.Models.Producto", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Cantidad");

                    b.Property<string>("Caracteristicas");

                    b.Property<int>("Color");

                    b.Property<string>("Nombre");

                    b.Property<decimal>("Precio");

                    b.HasKey("ID");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("LexiBalance.Models.Trabajador", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DNI");

                    b.Property<string>("Direccion");

                    b.Property<string>("Nombre");

                    b.Property<int>("Telefono");

                    b.HasKey("ID");

                    b.ToTable("Trabajador");
                });

            modelBuilder.Entity("LexiBalance.Models.Venta", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Cliente");

                    b.Property<DateTime>("Fecha");

                    b.Property<string>("Producto");

                    b.Property<string>("Trabajador");

                    b.HasKey("ID");

                    b.ToTable("Venta");
                });
#pragma warning restore 612, 618
        }
    }
}