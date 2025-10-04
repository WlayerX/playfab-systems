namespace Core.Domain {
    public class Player {
        public string Id { get; }
        public string DisplayName { get; private set; }

        public Player(string id, string displayName = "") {
            Id = id;
            DisplayName = displayName;
        }

        public void SetDisplayName(string name) {
            DisplayName = name;
        }
    }
}
