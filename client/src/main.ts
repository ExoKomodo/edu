import { createApp } from 'vue';
import { createAuth0 } from '@auth0/auth0-vue';

import App from './App.vue';
import router from './router';

import './index.css';

const app = createApp(App);

app.use(router);
app.use(
  createAuth0({
    domain: 'exokomodo.us.auth0.com',
    clientId: 'd0nbGyYvhTxPjyL1eaa3K4ojLDUNt1LX',
    authorizationParams: {
      redirect_uri: window.location.origin,
      audience: 'https://services.edu.exokomodo.com',
    },
    cacheLocation: 'localstorage',
  })
);

app.mount('#app');
