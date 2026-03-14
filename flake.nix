{
  description = "Husao";

  inputs = {
    nixpkgs.url = "github:NixOS/nixpkgs/nixos-unstable";
    deeriapkgs.url = "github:khanhtranrk/deeria";
  };

  outputs = { self, nixpkgs, deeriapkgs }:
  let
    system = "x86_64-linux";
    pkgs = import nixpkgs { inherit system; };
    deeria = deeriapkgs.packages.${system}.default;
  in
  {
    devShells.${system}.default = pkgs.mkShell {
      packages = with pkgs; [
        dotnet-sdk_10
        omnisharp-roslyn
        process-compose
        deeria
        nuget-to-json
      ];

      shellHook = ''
        export HUSAO_CONFIG_FILE=ZDEBUG/husao.json
        export HUSAO_DATA_PATH=ZDEBUG

        mkdir -p ZDEBUG

        if [ ! -f "ZDEBUG/husao.json" ]; then
        cat <<EOF > ZDEBUG/husao.json
        {
          "AESKey": "Ips8csbHer6nDwTqKNquJdLuU6wxJtJghJw4A/PC9EY=",
          "DeeriaBaseUrl": "http://127.0.0.1:4243",
          "DeeriaSeasonProxie": "husao-season",
          "DeeriaEpisodeProxie": "husao-episode"
        }
        EOF
        fi

        if [ ! -f "ZDEBUG/deeria.toml" ]; then
        cat <<EOF > ZDEBUG/deeria.toml
        [server]
        host = '127.0.0.1'
        port = 4243

        [proxies.husao-player]
        type = "remote"
        target = "https://exmaple.com"
        rewrite = { "\x68" = "\x86" }

        [proxies.husao-season]
        type = "local"
        target = "''$PWD/ZDEBUG/husao/cache/"
        rewrite = { "<location>" = "<location>http://127.0.0.1:4243/husao-episode/" }

        [proxies.husao-episode]
        type = "local"
        target = "''$PWD/ZDEBUG/husao/cache/"
        rewrite = { "https://example.com" = "http://127.0.0.1:4243/husao-player" }
        EOF
        fi
      '';
    };

    packages.${system}.default =
      pkgs.buildDotnetModule {
        pname = "husao";
        version = "0.1.0";

        src = ./.;

        projectfile = "Husao.slnx";
        nugetDeps = ./deps.json;

        dotnet-sdk = pkgs.dotnet-sdk_10;
        dotnet-runtime = pkgs.dotnet-runtime_10;

        enableParallelBuilding = false; 
  
        dotnetFlags = [ 
          "-p:MaxCpuCount=1" 
        ];

        nativeBuildInputs = [ pkgs.makeWrapper ];

        executables = [
          "hs"
        ];
      };

    apps.${system}.default = {
      type = "app";
      program = "${self.packages.${system}.default}/bin/hs";
    };
  };
}
