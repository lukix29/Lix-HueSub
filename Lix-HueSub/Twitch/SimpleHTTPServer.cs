// MIT License - Copyright (c) 2016 Can Güney Aksakalli

using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

internal class Token_HTTP_Server : IDisposable
{
    private const string tokenKey = "token=";

    private readonly int _port;
    private HttpListener _listener;
    private Task _serverThread;
    private bool _stopServer = false;

    /// <summary>
    /// Construct server with given port.
    /// </summary>
    /// <param name="path">Directory path to serve.</param>
    /// <param name="port">Port of the server.</param>
    public Token_HTTP_Server(int port)
    {
        this._port = port;
    }

    public delegate void OnTokenReceived(string Token);

    public event OnTokenReceived ReceivedToken;

    public bool IsStarted
    {
        get;
        private set;
    }

    public void Dispose()
    {
        try
        {
            _listener?.Close();
            _serverThread?.Dispose();
        }
        catch { }
    }

    public void Start()
    {
        if (!IsStarted)
        {
            _stopServer = false;
            _serverThread = Task.Run(() => this.Listen());
            IsStarted = true;
        }
    }

    /// <summary>
    /// Stop server and dispose all functions.
    /// </summary>
    public void Stop()
    {
        _stopServer = true;
        IsStarted = false;
        Dispose();
    }

    private void Listen()
    {
        try
        {
            _listener?.Close();
            _listener = new HttpListener();

            _listener.Prefixes.Add("http://localhost:" + _port + "/");
            _listener.Start();
            while (true)
            {
                if (_stopServer)
                {
                    _listener.Close();
                    return;
                }
                HttpListenerContext context = _listener.GetContext();
                Process(context);
            }
        }
        catch (Exception ex)
        {
            if (IsStarted)
            {
                if (LX29_Tools.HasInternetConnection)
                {
                    Listen();
                }
                else
                {
                    if (!(ex is ThreadAbortException))
                    {
                        switch (ex.Handle())
                        {
                            case System.Windows.Forms.MessageBoxResult.Retry:
                                Listen();
                                break;
                        }
                    }
                }
            }
        }
    }

    private void Process(HttpListenerContext context)
    {
        string filename = context.Request.Url.ToString();
        string sessionid = "";

        if (!string.IsNullOrEmpty(filename))
        {
            if (filename.Contains(tokenKey))
            {
                WriteFile(LixHueSub.Properties.Resources.success, context);
                Stop();

                sessionid = filename.Substring(filename.IndexOf(tokenKey) + tokenKey.Length);

                ReceivedToken?.Invoke(sessionid);
            }
            else WriteFile(LixHueSub.Properties.Resources.redirect, context);
        }
    }

    private void WriteFile(string input, HttpListenerContext context)
    {
        if (!string.IsNullOrEmpty(input))
        {
            try
            {
                byte[] data = System.Text.Encoding.UTF8.GetBytes(input);

                context.Response.ContentType = "mime";
                context.Response.ContentLength64 = data.LongLength;
                context.Response.AddHeader("Date", DateTime.Now.ToString("r"));
                context.Response.AddHeader("Last-Modified", DateTime.Now.ToString("r"));

                context.Response.OutputStream.Write(data, 0, data.Length);

                context.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Response.OutputStream.Flush();
            }
            catch
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }
        else
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
        }
    }
}