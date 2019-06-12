namespace SAJESS.Manager

{
 public   class ResponseModel
    {
        public ResponseModel()
        {
            Message = "An error has occured while data get !";
        }
        public bool Status { get; set; }
        public string Message { get; set; }

        public object Data { get; set; }
        public ResponseModel Respons(bool isSuccess, string msg)
        {
            return new ResponseModel { Status = isSuccess, Message = msg, Data = null };
        }
        public ResponseModel Respons(object data)
        {
            return new ResponseModel { Status = true, Data = data };
        }
    }
}
