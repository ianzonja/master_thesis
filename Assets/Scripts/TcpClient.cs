using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class TcpKlijent
{
    TcpClient klijent;
    Int32 port = 9950;
    NetworkStream stream;
    byte[] readBuffer;
    byte[] writeBuffer;

    /// <summary>
    /// Konstruktor klase TcpKlijent
    /// </summary>
    public TcpKlijent()
    {
        try
        {
            klijent = new TcpClient();
            klijent.Connect(IPAddress.Loopback, port);
        }
        catch (Exception)
        {
            Debug.Log("Ne mogu se spojiti na servis.");
        }
    }

    /// <summary>
    /// Metoda koja šalje serveru objekt - taj objekt serijalizira u json te ga zapisuje na stream sa kojega server
    /// može pročitati podatke.
    /// </summary>
    /// <param name="objekt"></param>
    /// <param name="tipPoruke"></param>
    public void PosaljiServeru(string json)
    {
        try
        {
            stream = klijent.GetStream();
            //string jsonString = JsonPretvarac.Serijalizacija(objekt, tipPoruke);
            int length = Encoding.UTF8.GetBytes(json).Length;
            writeBuffer = new byte[length];
            writeBuffer = Encoding.UTF8.GetBytes(json);
            stream.Write(writeBuffer, 0, writeBuffer.Length);
        }
        catch (Exception)
        {
            //MessageBox.Show("Problemi oko komunikacije sa web servisom, pokušajte ponovo kasnije");
            return;
        }
        stream.Flush();
    }

    /// <summary>
    /// Metoda koja prima json od servera u chunkovima te konsturira kompletnu poruku sve dok su podaci na streamu dohvatljivi.
    /// Deserijalizira json u odgovarajući podatkovni objekt.
    /// </summary>
    /// <returns></returns>
    public string PrimiOdServera()
    {
        readBuffer = new byte[1024];
        int numberOfBytesRead = 0;
        StringBuilder myCompleteMessage = new StringBuilder();
        string jsonString = "";
        try
        {
            stream = klijent.GetStream();
            do
            {
                numberOfBytesRead = stream.Read(readBuffer, 0, readBuffer.Length);
                stream.Flush();
                myCompleteMessage.AppendFormat("{0}", Encoding.UTF8.GetString(readBuffer, 0, numberOfBytesRead));
            } while (stream.DataAvailable);
            stream.Close();
            jsonString = myCompleteMessage.ToString();
            //trenutni = JsonPretvarac.Deserijalizacija(jsonString);
            stream.Flush();
        }
        catch (Exception)
        {
            Debug.Log("Problemi sa servisom. Nisam uspio primiti od servera poruku");
        }
        return jsonString;
    }

    /// <summary>
    /// Metoda koja zatvara otvoreni socket između klijenta i servera
    /// </summary>
    public void ZatvoriSocket()
    {
        klijent.Close();
    }
}
