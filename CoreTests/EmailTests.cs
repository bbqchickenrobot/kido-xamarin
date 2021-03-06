using System;
using System.Linq;
using System.Net;
using NUnit.Framework;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

using KidoZen;

namespace KidoZen.Core.Tests
{
	[TestFixture]
	public class EmailTests
	{
		static KZApplication app;
		static MailSender mail;

		[SetUp]
		public void SetUp ()
		{
			Console.WriteLine ("Setting up");
			Stopwatch sw = new Stopwatch();
			sw.Start();

			if (app==null) {
				app = new KZApplication(Constants.marketplace, Constants.application,Constants.applicationKey);
				app.Initialize().Wait();
				app.Authenticate(Constants.user, Constants.pass, Constants.provider).Wait();
			}
			sw.Stop();
			Console.WriteLine("Elapsed={0}",sw.Elapsed);

			if (mail == null) {
				mail = app.MailSender;
			}
		}

		[Test]
		public void CanGetAnInstance()
		{
			Assert.AreEqual(Constants.appUrl + "/email", mail.Url.ToString());
		}

		[Test]
		public void Send()
		{
			var result = mail.Send(new Mail {
				from = Constants.email,
				to = Constants.email,
				subject = "test from Xamarin SDK",
				textBody ="does it work?",
				htmlBody = "<html><body><a>does it work?</a></body></html>"
			}).Result;
			Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
		}
	}
}