import { createBrowserHistory } from 'history';
const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
export const StoreHistory = createBrowserHistory({ basename: baseUrl });
