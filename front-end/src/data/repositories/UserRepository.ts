import Repository from "../Repository";
import { User } from "../entities/User";

export default class TestRepository extends Repository<User> {
  constructor() {
    super("User");
    console.log("construidad, testrepository");
  }
}
