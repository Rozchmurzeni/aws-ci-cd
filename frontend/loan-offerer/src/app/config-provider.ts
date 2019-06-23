import { Injectable } from "@angular/core";
import { environment } from "../environments/environment";

@Injectable({
  providedIn: "root"
})
export class ConfigProvider {
  constructor() {}

  loanOfferApiUrl(): string {
    return environment.loanOffererApiBaseUrl;
  }

  loanOfferApiKey(): string {
    return environment.loanOffererApiKey;
  }
}
