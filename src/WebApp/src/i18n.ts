import i18n from "i18next";
import LanguageDetector from "i18next-browser-languagedetector";

import * as en from "./locales/en.json"
import * as hu from "./locales/hu.json"

i18n.use(LanguageDetector).init({
    fallbackLng: "en",
    debug: false,
    keySeparator: '.',
    ns: ["translations"],
    defaultNS: "translations",
    resources: {}
});

i18n.addResourceBundle('en', 'translations', en)
i18n.addResourceBundle('hu', 'translations', hu)

export default i18n;