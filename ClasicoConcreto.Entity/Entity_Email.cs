// Decompiled with JetBrains decompiler
// Type: ClasicoConcreto.Entity.Entity_Email
// Assembly: ClasicoConcreto.Entity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 88FB6C84-08B1-4E89-936D-55DC20DF6C02
// Assembly location: C:\Users\jsoto\Desktop\Recuperacion Concreto\ClasicoConcreto.Entity.dll

using System.Collections.Generic;
using System.Net.Mail;

namespace ClasicoConcreto.Entity
{
  public class Entity_Email
  {
    private MailAddress _strFrom;
    private List<MailAddress> _lstTo;
    private List<MailAddress> _lstBBC;
    private List<MailAddress> _lstCC;
    private string _strSubject;
    private string _strContenido;
    private List<string> _lstAttachmentPath;

    public Entity_Email()
    {
      this._lstTo = new List<MailAddress>();
      this._lstBBC = new List<MailAddress>();
      this._lstCC = new List<MailAddress>();
      this._lstAttachmentPath = new List<string>();
    }

    public Entity_Email(
      MailAddress strFrom,
      string strContenido,
      List<MailAddress> lstTo,
      string strSubject,
      List<string> lstAttachmentPath)
    {
      this._strFrom = strFrom;
      this._strContenido = strContenido;
      this._lstTo = lstTo;
      this._lstBBC = new List<MailAddress>();
      this._lstCC = new List<MailAddress>();
      this._strSubject = strSubject;
      this._lstAttachmentPath = lstAttachmentPath;
    }

    public Entity_Email(
      MailAddress strFrom,
      string strContenido,
      List<MailAddress> lstTo,
      List<MailAddress> lstCC,
      string strSubject,
      List<string> lstAttachmentPath)
    {
      this._strFrom = strFrom;
      this._strContenido = strContenido;
      this._lstTo = lstTo;
      this._lstBBC = new List<MailAddress>();
      this._lstCC = lstCC;
      this._strSubject = strSubject;
      this._lstAttachmentPath = lstAttachmentPath;
    }

    public MailAddress strFrom
    {
      get
      {
        return this._strFrom;
      }
      set
      {
        this._strFrom = value;
      }
    }

    public List<MailAddress> lstTo
    {
      get
      {
        return this._lstTo;
      }
      set
      {
        this._lstTo = value;
      }
    }

    public List<MailAddress> lstBBC
    {
      get
      {
        return this._lstBBC;
      }
      set
      {
        this._lstBBC = value;
      }
    }

    public List<MailAddress> lstCC
    {
      get
      {
        return this._lstCC;
      }
      set
      {
        this._lstCC = value;
      }
    }

    public string strSubject
    {
      get
      {
        return this._strSubject;
      }
      set
      {
        this._strSubject = value;
      }
    }

    public string strContenido
    {
      get
      {
        return this._strContenido;
      }
      set
      {
        this._strContenido = value;
      }
    }

    public List<string> lstAttachmentPath
    {
      get
      {
        return this._lstAttachmentPath;
      }
      set
      {
        this._lstAttachmentPath = value;
      }
    }
  }
}
