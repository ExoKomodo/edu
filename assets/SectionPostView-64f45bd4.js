import{d as y,u as _,b as h,o,f as s,a as b,k as a,t as m,l as d,g as u,j as x,A as k,e as w}from"./index-3492b608.js";import{_ as g}from"./SectionEditor.vue_vue_type_script_setup_true_lang-c5aa75bd.js";import{S as A}from"./SectionService-15405545.js";import{S}from"./Spinner-3a9e1c20.js";const v={class:"sectionPostBackground min-h-screen"},T={class:"max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-4 text-white"},I={key:0,class:"text-2xl font-bold border-slate-400 rounded border-2 p-1 pl-2"},L=["innerHTML"],D={key:2,class:"text-xl border-slate-400 rounded border-2 p-1 pl-2 my-2"},M=["innerHTML"],N=y({__name:"SectionPost",props:{id:{},courseId:{},name:{},content:{},description:{},difficulty:{}},setup(r){const n=x(),c=_(),e=r,t=h({isEditMode:!1,showPreview:!1,id:e.id,name:e.name,content:e.content,description:e.description,difficulty:e.difficulty});async function l(i){const p={id:e.id,content:i.content,difficulty:Number.parseInt(i.difficulty),metadata:{name:i.name,description:i.description,courseId:e.courseId}};await A.updateAsync(p,{toast:c,token:await k.getAccessTokenAsync(n,{toast:c})}),window.location.reload()}return(i,p)=>{var f;return o(),s("div",v,[b("div",T,[a(n).isAuthenticated?(o(),s("p",I,m((f=t.name)==null?void 0:f.toUpperCase()),1)):d("",!0),a(n).isAuthenticated?(o(),s("p",{key:1,class:"text-xl border-slate-400 rounded border-2 p-1 pl-2 my-2",innerHTML:t.description},null,8,L)):d("",!0),a(n).isAuthenticated?(o(),s("p",D,"Difficulty: "+m(t.difficulty),1)):d("",!0),a(n).isAuthenticated?(o(),s("p",{key:3,class:"text-xl border-slate-400 rounded border-2 p-1 pl-2 my-2",innerHTML:t.content},null,8,M)):d("",!0),u(g,{handler:l,handlerText:"Update",sectionId:t.id,sectionContent:t.content,sectionDifficulty:t.difficulty,sectionDescription:t.description,sectionName:t.name},null,8,["sectionId","sectionContent","sectionDifficulty","sectionDescription","sectionName"])])])}}}),C={key:0,class:"flex place-content-center"},B={key:1},U=y({__name:"SectionPostView",props:{id:{},courseId:{}},setup(r){const n=x(),c=r,e=h({isLoading:!0,section:{}}),t=_();return w(async()=>{try{e.section=await A.getAsync(c.id,{toast:t,token:await k.getAccessTokenAsync(n)})}finally{e.isLoading=!1}}),(l,i)=>e.isLoading?(o(),s("div",C,[u(S)])):(o(),s("div",B,[u(N,{id:l.id,name:e.section.metadata.name,content:e.section.content,difficulty:e.section.difficulty,description:e.section.metadata.description,"course-id":c.courseId},null,8,["id","name","content","difficulty","description","course-id"])]))}});export{U as default};