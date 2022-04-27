import Repository from "../Repository";
import { User } from "../entities/User";

export default class UserRepository extends Repository<User> {
  constructor() {
    super("User");
  }
}
