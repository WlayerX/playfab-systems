handlers.getInventory = function(args, context) {
    var playfabId = args.PlayFabId || context.playerId;
    // For demo, return mock inventory. Replace with real server-side validation or DB calls.
    return { Items: [ { ItemId: "sword_01", Count: 1 }, { ItemId: "potion_small", Count: 5 } ] };
};
