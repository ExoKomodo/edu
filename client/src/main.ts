import { createApp } from 'vue';
import { createAuth0 } from '@auth0/auth0-vue';
import { VAceEditor } from 'vue3-ace-editor';

import HttpServiceV1 from '@/services/HttpServiceV1';

import App from './App.vue';
import router from './router';

import './index.css';

const app = createApp(App);

app.component('VAceEditor', VAceEditor);

app.use(router);
app.use(
  createAuth0({
    domain: HttpServiceV1.auth0BaseUrl,
    clientId: 'd0nbGyYvhTxPjyL1eaa3K4ojLDUNt1LX',
    authorizationParams: {
      redirect_uri: window.location.origin,
      audience: 'https://services.edu.exokomodo.com',
    },
    cacheLocation: 'localstorage',
  })
);

app.mount('#app');

import ace from 'ace-builds';
import 'ace-builds/src-noconflict/ext-language_tools';
import modeHtmlUrl from 'ace-builds/src-noconflict/mode-html?url';
import modeJavascriptUrl from 'ace-builds/src-noconflict/mode-javascript?url';
import modeJsonUrl from 'ace-builds/src-noconflict/mode-json?url';
import modePlainTextUrl from 'ace-builds/src-noconflict/mode-plain_text?url';
import modePythonUrl from 'ace-builds/src-noconflict/mode-python?url';
import modeYamlUrl from 'ace-builds/src-noconflict/mode-yaml?url';
import snippetsHtmlUrl from 'ace-builds/src-noconflict/snippets/html?url';
import snippetsJsonUrl from 'ace-builds/src-noconflict/snippets/json?url';
import snippetsJsUrl from 'ace-builds/src-noconflict/snippets/javascript?url';
import snippetsPythonUrl from 'ace-builds/src-noconflict/snippets/python?url';
import snippetsYamlUrl from 'ace-builds/src-noconflict/snippets/yaml?url';
import themeChromeUrl from 'ace-builds/src-noconflict/theme-chrome?url';
import themeGithubUrl from 'ace-builds/src-noconflict/theme-github?url';
import themeMonokaiUrl from 'ace-builds/src-noconflict/theme-monokai?url';
import themeTomorrowNightEighties from 'ace-builds/src-noconflict/theme-tomorrow_night_eighties';
import workerBaseUrl from 'ace-builds/src-noconflict/worker-base?url';
import workerHtmlUrl from 'ace-builds/src-noconflict/worker-html?url';
import workerJavascriptUrl from 'ace-builds/src-noconflict/worker-javascript?url';
import workerJsonUrl from 'ace-builds/src-noconflict/worker-json?url';
import workerYamlUrl from 'ace-builds/src-noconflict/worker-yaml?url';

function enableAceEditorHtml() {
  ace.config.setModuleUrl('ace/mode/base', workerBaseUrl);
  ace.config.setModuleUrl('ace/mode/html', modeHtmlUrl);
  ace.config.setModuleUrl('ace/mode/html_worker', workerHtmlUrl);
  ace.config.setModuleUrl('ace/mode/html_worker', workerHtmlUrl);
  ace.config.setModuleUrl('ace/mode/javascript', modeJavascriptUrl);
  ace.config.setModuleUrl('ace/mode/javascript_worker', workerJavascriptUrl);
  ace.config.setModuleUrl('ace/mode/json', modeJsonUrl);
  ace.config.setModuleUrl('ace/mode/json_worker', workerJsonUrl);
  ace.config.setModuleUrl('ace/mode/plain_text', modePlainTextUrl);
  ace.config.setModuleUrl('ace/mode/python', modePythonUrl);
  ace.config.setModuleUrl('ace/mode/yaml', modeYamlUrl);
  ace.config.setModuleUrl('ace/mode/yaml_worker', workerYamlUrl);
  ace.config.setModuleUrl('ace/snippets/html', snippetsHtmlUrl);
  ace.config.setModuleUrl('ace/snippets/javascript', snippetsJsUrl);
  ace.config.setModuleUrl('ace/snippets/javascript', snippetsYamlUrl);
  ace.config.setModuleUrl('ace/snippets/json', snippetsJsonUrl);
  ace.config.setModuleUrl('ace/snippets/python', snippetsPythonUrl);
  ace.config.setModuleUrl('ace/theme/chrome', themeChromeUrl);
  ace.config.setModuleUrl('ace/theme/github', themeGithubUrl);
  ace.config.setModuleUrl('ace/theme/monokai', themeMonokaiUrl);
  ace.config.setModuleUrl('ace/theme/tomorrow_night_eighties', themeTomorrowNightEighties);

  ace.require("ace/ext/language_tools");
}

function enableAceEditorLanguages() {
  enableAceEditorHtml();
}

enableAceEditorLanguages()