namespace SoggyBot.Classes {
    class MethodResult {

        public bool Status { get; set; }

        public object Result { get; set; }

        public MethodResult() {

        }
        public MethodResult(bool stat, object res) {
            Status = stat;
            Result = res;
        }
    }
}
