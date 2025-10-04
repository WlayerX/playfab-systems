/**
 * PlayFab CloudScript: auth helper example
 * Deploy this code via PlayFab Admin API or PlayFab CLI.
 *
 * Exports:
 *  - loginWithCustomId(params): expects { CustomId }
 *
 * NOTE: validate inputs & add proper error handling in production.
 */

handlers.loginWithCustomId = function(args, context) {
    var customId = args.CustomId || null;
    if (!customId) {
        return { error: "Missing CustomId" };
    }

    // Example: return a custom payload; real auth should call PlayFab APIs server-side if needed
    return { PlayFabId: "CLOUD_" + customId, Message: "Logged in via CloudScript" };
};
