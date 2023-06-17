db.createUser({
  user: "test",
  pwd:  "***********",
  roles: [
    {
      role: "dbOwner",
      db: "test" 
    }
  ]
});
