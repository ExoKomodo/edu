var o=Object.defineProperty;var y=(a,r,t)=>r in a?o(a,r,{enumerable:!0,configurable:!0,writable:!0,value:t}):a[r]=t;var i=(a,r,t)=>(y(a,typeof r!="symbol"?r+"":r,t),t);import{S as n,H as s,E as w}from"./index-3492b608.js";class h{constructor(){i(this,"objectViewKeys",[{key:"id",kind:"text"}])}make(){return new n({})}static async createAsync(r,t={}){var e;try{return new n(await s.postAsync("section",r,t))}catch(c){throw(e=t.toast)==null||e.error(`Failed to create section: ${c}`),c}}static async deleteAsync(r,t={}){var e;try{return new n(await s.deleteAsync("section",r,t))}catch(c){throw(e=t.toast)==null||e.error(`Failed to delete section: ${c}`),c}}static async getAsync(r,t={}){var e;try{return new n(await s.getAsync("section",r,t))}catch(c){throw(e=t.toast)==null||e.error(`Failed to get section: ${c}`),c}}static async getAllAsync(r={}){var t;try{const e=await s.getAllAsync("section",r);return Object.keys(e).forEach(function(c,l){e[c]=new w(e[c])}),new Map(Object.entries(e))}catch(e){throw(t=r.toast)==null||t.error(`Failed to get all sections: ${e}`),e}}static async updateAsync(r,t={}){var e;try{return new n(await s.putAsync("section",r,t))}catch(c){throw(e=t.toast)==null||e.error(`Failed to update section: ${c}`),c}}}export{h as S};
