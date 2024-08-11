using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class Message2Manager : IMessage2Service
    {
        IMessage2Dal _message2Dal;

        public Message2Manager(IMessage2Dal message2Dal)
        {
            _message2Dal = message2Dal; 
        }

        public List<Message2> GetInboxListByWriter(int id)
        {
            //  return _message2Dal.GetListAll(x => x.ReceiverID == id); // id ye göre mesajlar geliyor
            return _message2Dal.GetInboxListWithMessageByWriter(id);   // EfMessage2Repository da mesajı gönderene erişiyor.

        }

        public List<Message2> GetList()
        {
          return _message2Dal.GetListAll();
        }

        public List<Message2> GetSendboxListByWriter(int id)
        {
            return _message2Dal.GetSendBoxListWithMessageByWriter(id);   // EfMessage2Repository da mesajı gönderene erişiyor.
        }

        public void TAdd(Message2 t)
        {
            _message2Dal.Insert(t);
        }

        public void TDelete(Message2 t)
        {
            throw new NotImplementedException();
        }

        public Message2 TGetById(int id)
        {
            return _message2Dal.GetById(id);
        }

        public void TUpdate(Message2 t)
        {
            throw new NotImplementedException();
        }
    }
}
