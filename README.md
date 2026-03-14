# Husao

A media librarian tool designed to manage and play content from Library Sii. It acts as a controller that interfaces with deeria for execution.

## Requirements
- **deeria**: Required for handling `play` commands and proxying media streams.
- **Nix**: The project is managed and distributed via Nix Flakes.

## Execution

Run the application directly using Nix:
```bash
nix run "github:khanhtranrk/husao"
```

## Configuration

Default config path: `~/.config/husao/config.json`.
Override using the `HUSAO_CONFIG_FILE` environment variable.

### Husao Schema (JSON)

- AESKey - Key used for data decryption
- deeriaBaseUrl - Target URL for the deeria instance (e.g., http://127.0.0.1:4243)
- deeriaSeasonProxie - Proxy identifier for season-level data
- deeriaEpisodeProxie - Proxy identifier for episode-level data

Example:
```json
{
  "AESKey": "Ips8csbHer6nDwTqKNquJdLuU6wxJtJghJw4A/PC9EY=",
  "DeeriaBaseUrl": "http://127.0.0.1:4243",
  "DeeriaSeasonProxie": "husao-season",
  "DeeriaEpisodeProxie": "husao-episode"
}
```

### Deeria Integration

Husao requires specific proxy definitions within the deeria configuration file to map local caches and remote targets.

Example:
```toml
[server]
host = '127.0.0.1'
port = 4243

[proxies.husao-player]
type = "remote"
target = "https://exmaple.com"
rewrite = { "\x68" = "\x86" }

[proxies.husao-season]
type = "local"
target = "/home/user/.config/husao/cache/"
rewrite = { "<location>" = "<location>http://127.0.0.1:4243/husao-episode/" }

[proxies.husao-episode]
type = "local"
target = "/home/user/.config/husao/cache/"
rewrite = { "https://example.com" = "http://127.0.0.1:4243/husao-player" }
```

## Development

Use nix develop to enter the development shell.
- ZDEBUG Directory: Automatically initialized upon entering the shell.
- Isolation: Contains localized config and application data for debugging purposes.
- Testing with deeria: Run process-compose up to launch the deeria service.
  - It automatically loads the configuration from the ZDEBUG directory.
  - Service listens on port 4243 by default.
- Logic: Environment behavior and directory mapping are defined within flake.nix.

## Environment Variables

- HUSAO_CONFIG_FILE: Explicit path to the JSON configuration file.
- HUSAO_DATA_PATH: Directory path for application data storage.
