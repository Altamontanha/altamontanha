using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using NHibernate;
using NHibernate.Cfg;

namespace AltaMontanha.NHibernate
{
	public class HttpModule : IHttpModule
	{
		#region Atributos / Propriedades

		public static readonly string CHAVE = "NHibernateSession";

		private static ISession sessao;
		public static ISession RecuperarSessao
		{
			get
			{
				if (HttpContext.Current == null)
				{
					if (HttpModule.sessao == null)
						HttpModule.sessao = AbrirSessao();

					return HttpModule.sessao;
				}
				else
				{
					HttpContext currentContext = HttpContext.Current;
					ISession sessao = currentContext.Items[CHAVE] as ISession;

					if (sessao == null)
					{
						sessao = AbrirSessao();
						currentContext.Items[CHAVE] = sessao;
					}

					return sessao;
				}
			}
		}

		private static ISessionFactory fabrica = null;

		#endregion
		
		#region Eventos

		private void ContextBeginRequest(object sender, EventArgs e)
		{
			HttpApplication application = (HttpApplication)sender;
			HttpContext context			= application.Context;
			context.Items[CHAVE]		= AbrirSessao();
		}

		private void ContextEndRequest(object sender, EventArgs e)
		{
			HttpApplication application = (HttpApplication)sender;
			HttpContext context			= application.Context;

			ISession session = (ISession) context.Items[CHAVE];

			if (session != null)
			{
				if (session.IsConnected)
				{
					session.Flush();
					session.Close();
				}
			}

			context.Items[CHAVE] = null;
		}

		#endregion

		#region Métodos
		
		public void Init(HttpApplication context)
		{
			context.BeginRequest += new EventHandler(ContextBeginRequest);
			context.EndRequest += new EventHandler(ContextEndRequest);
		}

		private static ISession AbrirSessao()
		{
			ISession session;
			session = GetFactory().OpenSession();

			if (session == null)
				throw new InvalidOperationException("OpenSession() is null.");

			return session;
		}

		private static ISessionFactory GetFactory()
		{
			if (fabrica == null)
			{
				Configuration config = new Configuration();

				if (config == null)
					throw new InvalidOperationException("NHibernate configuration é nulo.");

				config.Configure();

				fabrica = config.BuildSessionFactory();

				if (fabrica == null)
					throw new InvalidOperationException("BuildSessionFactory é nulo.");
			}

			return fabrica;

		}

		public void Dispose()
		{
		}

		#endregion
	}
}
