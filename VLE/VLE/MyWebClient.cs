using System;
using System.Net;

public class MyWebClient : WebClient
{
    protected override WebRequest GetWebRequest(Uri uri)
    {
        WebRequest w = base.GetWebRequest(uri);
        w.Timeout = 3000;
        return w;
    }
}
