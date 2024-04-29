import{d as l,_ as u,r as g,o as t,c as d,w as m,a as i,t as c,b as x,u as f,e as y,f as n,g as h,F as b,h as k}from"./index-3492b608.js";import{B as v}from"./BlogService-136a06a7.js";import{S as B}from"./Spinner-3a9e1c20.js";const L=l({name:"BlogLink",props:{title:{type:String,required:!0},description:{type:String,required:!0},id:{type:String,required:!0}}});const S={class:"text-2xl"},$={class:"text-gray-400"};function w(o,e,r,_,p,a){const s=g("RouterLink");return t(),d(s,{to:`/blog/${o.id}`,class:"hover:bg-midnightGreen flex flex-col transition duration-250"},{default:m(()=>[i("p",S,c(o.title),1),i("p",$,c(o.description)+"...",1)]),_:1},8,["to"])}const I=u(L,[["render",w],["__scopeId","data-v-084f6292"]]),q={class:"blogBackground min-h-screen"},C={class:"max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-5 my-5"},V=i("h1",{class:"p-2 bg-mysticStone text-white rounded flex justify-center text-3xl font-bold my-3"},"blogs",-1),A={key:0,class:"flex place-content-center"},F={key:1},D=l({__name:"BlogView",setup(o){const e=x({isLoading:!0,blogIndex:{}}),r=f();return y(async()=>{try{e.blogIndex=await v.getAllAsync({toast:r}),console.log(e.blogIndex)}finally{e.isLoading=!1}}),(_,p)=>(t(),n("div",q,[i("div",C,[V,e.isLoading?(t(),n("div",A,[h(B)])):(t(),n("div",F,[(t(!0),n(b,null,k(e.blogIndex.blogs,([a,s])=>(t(),d(I,{class:"p-2 bg-mysticStone text-white rounded flex pl-5 my-3",id:a,title:s.title,description:s.description},null,8,["id","title","description"]))),256))]))])]))}});export{D as default};
