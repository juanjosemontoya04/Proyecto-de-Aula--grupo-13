using System;
using System.Collections.Generic;
using static System.Console;
using static System.Convert;

public class Nodo<T>
{
    public T valor { get; set; }
    public Nodo<T> siguiente { get; set; }

    public Nodo(T val)
    {
        valor = val;
        siguiente = null;
    }
}

public class Prenda
{
    public string tipo { get; set; }
    public string color { get; set; }
    public string tejido { get; set; }
    public string detalles { get; set; }

    public Prenda(string tipo, string color, string tejido, string detalles)
    {
        this.tipo = tipo;
        this.color = color;
        this.tejido = tejido;
        this.detalles = detalles;
    }
}

public class listaenlzprendas
{
    private Nodo<Prenda> cabeza;

    public listaenlzprendas()
    {
        cabeza = null;
    }

    public void AgregarPrenda(Prenda prenda)
    {
        if (cabeza == null)
        {
            cabeza = new Nodo<Prenda>(prenda);
        }
        else
        {
            AgregarPrendaRec(cabeza, prenda);
        }
    }

    private void AgregarPrendaRec(Nodo<Prenda> nodoActual, Prenda prenda)
    {
        if (nodoActual.siguiente == null)
        {
            nodoActual.siguiente = new Nodo<Prenda>(prenda);
        }
        else
        {
            AgregarPrendaRec(nodoActual.siguiente, prenda);
        }
    }

    public List<Prenda> ObtenerPrendas()
    {
        List<Prenda> prendas = new List<Prenda>();
        ObtenerPrendasRec(cabeza, prendas);
        return prendas;
    }

    private void ObtenerPrendasRec(Nodo<Prenda> nodoActual, List<Prenda> prendas)
    {
        if (nodoActual != null)
        {
            prendas.Add(nodoActual.valor);
            ObtenerPrendasRec(nodoActual.siguiente, prendas);
        }
    }
}

public class Usuario
{
    public string correo { get; set; }
    public string contraseña { get; set; }
    public listaenlzprendas prendasdiseñadas { get; set; } 

    public Usuario(string correo, string contraseña)
    {
        this.correo = correo;
        this.contraseña = contraseña;
        prendasdiseñadas = new listaenlzprendas();
    }
}

public class ListaEnlazadaUsuarios
{
    private Nodo<Usuario> cabeza;

    public ListaEnlazadaUsuarios()
    {
        cabeza = null;
    }

    public void AgregarUsuario(Usuario usuario)
    {
        if (cabeza == null)
        {
            cabeza = new Nodo<Usuario>(usuario);
        }
        else
        {
            AgregarUsuarioRec(cabeza, usuario);
        }
    }

    private void AgregarUsuarioRec(Nodo<Usuario> nodoActual, Usuario user)
    {
        if (nodoActual.siguiente == null)
        {
            nodoActual.siguiente = new Nodo<Usuario>(user);
        }
        else
        {
            AgregarUsuarioRec(nodoActual.siguiente, user);
        }
    }

    public Usuario BuscarUsuario(string correo)
    {
        return BuscarUsuarioRec(cabeza, correo);
    }

    private Usuario BuscarUsuarioRec(Nodo<Usuario> nodoActual, string correo)
    {
        if (nodoActual == null)
            return null;
        if (nodoActual.valor.correo == correo)
            return nodoActual.valor;

        return BuscarUsuarioRec(nodoActual.siguiente, correo);
    }
}

public class controlarusuario
{
    private ListaEnlazadaUsuarios usuarios;

    public controlarusuario()
    {
        usuarios = new ListaEnlazadaUsuarios();
    }

    public bool RegistrarUsuario(string correo, string contraseña)
    {
        if (usuarios.BuscarUsuario(correo) == null)
        {
            Usuario nuevo = new Usuario(correo, contraseña);
            usuarios.AgregarUsuario(nuevo);
            return true;
        }
        return false;
    }

    public Usuario IniciarSesion(string correo, string contraseña)
    {
        Usuario user = usuarios.BuscarUsuario(correo);
        if (user != null && user.contraseña == contraseña)
        {
            return user;
        }
        return null;
    }
}

public class controladordeprendas
{
    public void DiseñarPrenda(Usuario usuario, string tipo, string color, string tejido, string detalles)
    {
        Prenda nueva = new Prenda(tipo, color, tejido, detalles);
        usuario.prendasdiseñadas.AgregarPrenda(nueva);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        controlarusuario controldelosusuarios = new controlarusuario();
        controladordeprendas controlprendas = new controladordeprendas();
        Usuario usuarioActual = null;

        while (true)
        {
            WriteLine("1. Registrar Usuario");
            WriteLine("2. Iniciar Sesión");
            WriteLine("3. Diseñar Prenda");
            WriteLine("4. Salir");
            Console.Write("Seleccione una opción: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.Write("Ingrese correo: ");
                    string correo = Console.ReadLine();
                    Console.Write("Ingrese contraseña: ");
                    string contraseña = Console.ReadLine();

                    if (controldelosusuarios.RegistrarUsuario(correo, contraseña))
                    {
                        WriteLine("Usuario registrado exitosamente.");
                    }
                    else
                    {
                        WriteLine("El usuario ya existe.");
                    }
                    break;

                case "2":
                    Console.Write("Ingrese correo: ");
                    correo = Console.ReadLine();
                    Console.Write("Ingrese contraseña: ");
                    contraseña = Console.ReadLine();

                    usuarioActual = controldelosusuarios.IniciarSesion(correo, contraseña);

                    if (usuarioActual != null)
                    {
                        WriteLine("Inicio de sesión exitoso.");
                    }
                    else
                    {
                        WriteLine("Credenciales incorrectas.");
                    }
                    break;

                case "3":
                    if (usuarioActual != null)
                    {
                        Console.Write("Ingrese tipo de prenda: ");
                        string tipo = Console.ReadLine();
                        Console.Write("Ingrese color: ");
                        string color = Console.ReadLine();
                        Console.Write("Ingrese tejido: ");
                        string tejido = Console.ReadLine();
                        Console.Write("Ingrese detalles: ");
                        string detalles = Console.ReadLine();

                        controlprendas.DiseñarPrenda(usuarioActual, tipo, color, tejido, detalles);
                        WriteLine("Prenda diseñada exitosamente.");
                    }
                    else
                    {
                        WriteLine("Debe iniciar sesión primero.");
                    }
                    break;

                case "4":
                    WriteLine("Saliendo...");
                    return;

                default:
                    WriteLine("Opción no válida.");
                    break;
            }

            WriteLine();
        }
    }
}
